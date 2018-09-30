#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import os
import shutil
import unittest

from obox.box.repo_box import Box
from obox.config.obox_config import InitConfig


class TestCompiler(unittest.TestCase):
    def test_init(self):
        init_to_path = os.path.join(os.getcwd(), 'init')
        Box.init(init_to_path)
        init_config = InitConfig(init_to_path)
        self.assertTrue(os.path.exists(init_config.src_path()))
        self.assertTrue(os.path.exists(init_config.test_path()))
        self.assertTrue(os.path.exists(init_config.wallet_path()))
        self.assertTrue(os.path.exists(init_config.contract_path()))
        self.assertTrue(os.path.exists(os.path.join(init_config.test_path(), init_config.test_template_name())))
        shutil.rmtree(init_to_path)

    def test_generate_repo_url(self):
        target_repo_url = 'https://github.com/wdx7266/ontology-tutorialtoken.git'
        box_name = 'ontology-tutorialtoken'
        repo_url = Box.generate_repo_url(box_name)
        self.assertEqual(target_repo_url, repo_url)

    def test_git_clone(self):
        repo_to_path = os.path.join(os.getcwd(), 'test_clone')
        repo_url = 'https://github.com/wdx7266/ontology-tutorialtoken.git'
        Box.git_clone(repo_url, repo_to_path)
        try:
            shutil.rmtree(repo_to_path)
        except PermissionError:
            pass

    def test_unbox(self):
        box_name = 'ontology-tutorialtoken'
        box_to_path = os.path.join(os.getcwd(), 'test_unbox')
        Box.unbox(box_name, box_to_path)
        try:
            shutil.rmtree(box_to_path)
        except PermissionError:
            pass


if __name__ == '__main__':
    unittest.main()
