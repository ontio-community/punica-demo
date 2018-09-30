#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import shutil
import os
import re

import click
import git

from obox.exception.obox_exception import OBoxError, OBoxException
from obox.config.obox_config import InitConfig


class Box:
    @staticmethod
    def git_clone(repo_url: str, repo_to_path: str = ''):
        if repo_to_path == '':
            repo_to_path = os.getcwd()
        print('Downloading...')
        try:
            git.Repo.clone_from(url=repo_url, to_path=repo_to_path, depth=1)
        except git.GitCommandError as e:
            network_error = 'Could not read from remote repository'
            repo_exist_error = 'already exists and is not an empty directory'
            if network_error in str(e.args[2]):
                raise OBoxException(OBoxError.network_error)
            elif repo_exist_error in str(e.args[2]):
                raise OBoxException(OBoxError.repo_exist_error)
            else:
                raise OBoxException(OBoxError.other_error(e.args[2]))
        print('Unpacking...')
        try:
            os.remove(os.path.join(repo_to_path, '.gitignore'))
        except (PermissionError, FileNotFoundError):
            pass
        try:
            shutil.rmtree(os.path.join(repo_to_path, '.git'))
        except (PermissionError, FileNotFoundError):
            pass
        try:
            shutil.rmtree(os.path.join(repo_to_path, '.vscode'))
        except (PermissionError, FileNotFoundError):
            pass
        try:
            shutil.rmtree(os.path.join(repo_to_path, '.idea'))
        except (PermissionError, FileNotFoundError):
            pass

    @staticmethod
    def init(init_to_path: str):
        if init_to_path == '':
            init_to_path = os.getcwd()
        print('Unpacking...')
        init_config = InitConfig(init_to_path)
        os.makedirs(init_config.src_path())
        os.makedirs(init_config.test_path())
        os.makedirs(init_config.wallet_path())
        os.makedirs(init_config.contract_path())
        print('Setting up...')
        test_template = os.path.join(os.path.split(os.path.abspath(__file__))[0], init_config.test_template_name())
        shutil.copy(test_template, init_config.test_path())
        print('Unbox successful. Enjoy it!')

    @staticmethod
    def generate_repo_url(box_name: str) -> str:
        if re.match(r'^([a-zA-Z0-9-])+$', box_name):
            repo_url = ['https://github.com/wdx7266/', box_name, '.git']
        elif re.match(r'^([a-zA-Z0-9-])+/([a-zA-Z0-9-])+$', box_name) is None:
            repo_url = ['https://github.com/', box_name, '.git']
        else:
            raise OBoxException(OBoxError.invalid_box_name)
        return ''.join(repo_url)

    @staticmethod
    def unbox(box_name: str, repo_to_path: str = ''):
        repo_url = Box.generate_repo_url(box_name)
        Box.git_clone(repo_url, repo_to_path)
        print('Unbox successful. Enjoy it!')
