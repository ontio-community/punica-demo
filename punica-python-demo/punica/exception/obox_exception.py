#!/usr/bin/env python3
# -*- coding: utf-8 -*-


class OBoxException(Exception):
    def __init__(self, error: dict):
        super().__init__(error['code'], error['msg'])


class OBoxError:
    @staticmethod
    def get_error(code: int, msg: str) -> dict:
        error = dict()
        error['code'] = code
        error['msg'] = msg
        return error

    @staticmethod
    def other_error(msg: str) -> dict:
        return OBoxError.get_error(59000, "Other Error, " + msg)

    invalid_box_name = get_error.__func__(10000, 'box error, invalid box name')

    network_error = get_error.__func__(20000, 'please make sure you network state, and the repository exists.')

    repo_exist_error = get_error.__func__(30000, 'something already exists at the destination.')
    permission_error = get_error.__func__(30001, 'permission denied, please check your file path.')
