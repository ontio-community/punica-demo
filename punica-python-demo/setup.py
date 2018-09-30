#!/usr/bin/env python
# -*- coding: utf-8 -*-

from setuptools import setup, find_packages

setup(
    name='obox',
    version='0.0.0',
    description='Ontology Development Framework',
    long_description_markdown_filename='README.md',
    author='NashMiao',
    author_email='wdx7266@outlook.com',
    url='https://github.com/wdx7266/ontology-python-obox',
    maintainer='NashMiao',
    maintainer_email='wdx7266@outlook.com',
    license='GNU Lesser General Public License v3 (LGPLv3)',
    packages=find_packages(exclude=["tests", "tests.*"]),
    install_requires=[
        'ontology-python-sdk'
        'gitpython'
    ],
    python_requires='>=3.7, <4',
    platforms=["all"],
    classifiers=[
        'Development Status :: 1 - Planning',
        'Operating System :: OS Independent',
        'Intended Audience :: Developers',
        'License :: OSI Approved :: GNU Lesser General Public License v3 (LGPLv3)',
        'Programming Language :: Python :: 3.7',
    ],
    entry_points={
        'console_scripts': 'ontology-dapp-box=box.cli:main'
    }
)
