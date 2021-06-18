from typing import *
# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# @Time : 20212021/6/16 23:19
# @Author : DBY
# @File : spider_rest.py
import time
import random


def random_sleep(mu=0.5, sigma=0.001):
    '''正态分布随机睡眠
    :param mu: 平均值
    :param sigma: 标准差，决定波动范围
    '''
    secs = random.normalvariate(mu, sigma)
    if secs <= 0:
        secs = mu  # 太小则重置为平均值
    time.sleep(secs)
