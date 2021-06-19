from typing import *
# !/usr/bin/env python3
# -*- coding: utf-8 -*-
# @Time : 20212021/6/19 13:08
# @Author : DBY
# @File : graph.py
import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
# import pygal
import re

df = pd.read_csv(r'显卡.csv', encoding='utf-8')
items = list(df.columns)
items.remove('名称')
ddf = pd.DataFrame()


# def process(x):
#     nums = [int(i) for i in re.findall(r'\d+\.?\d*', str(x))]
#     if (l := len(nums)) == 0:
#         return 0
#     else:
#         return sum(nums) / l


# for i in ['性能评分']:
#     ddf[i] = df[i].apply(process)
#     max_ = ddf[i].max()
#     min_ = ddf[i].min()
#     ddf[i] = ddf[i].apply(lambda x: (x - min_) / (max_ - min_) * 5)
# ddf['名称'] = df['名称']
# ddf = df.loc[:, ['核心频率']].apply(lambda x: ','.join(re.findall(r'\d+\.?\d*', str(x))))
# print(ddf)
# print(df.head())
# x = '1365/1830MHz'
# y = ','.join(re.findall(r'\d+\.?\d*', str(x)))
# print(y)
class Graph:
    def __init__(self):
        plt.rcParams['font.family'] = ['sans-serif']
        plt.rcParams['font.sans-serif'] = ['SimHei']

    @staticmethod
    def bar(x=None, y=None, data: pd.DataFrame = None, limit=5):
        if True:
            # data = data.dropna(subset=[x])
            data = data.sort_values(by=y, ascending=True)
            g = sns.barplot(data=data.iloc[:limit, :], x=x, y=y)
            plt.xticks(fontsize=5)
            print(data.iloc[:limit, :]['性能评分'])
            g.set_xticklabels(g.get_xticklabels(), rotation=15)
            plt.savefig('rank.png')
        else:
            return False

    @staticmethod
    def plot_radar(data):
        '''
        the first column of the data is the cluster name;
        the second column is the number of each cluster;
        the last are those to describe the center of each cluster.
        '''
        kinds = data.iloc[:, 5]
        labels = data.iloc[:, :5].columns
        centers = pd.concat([data.iloc[:, :5], data.iloc[:, 0]], axis=1)
        centers = np.array(centers)
        n = len(labels)
        labels = list(labels.values)
        labels = labels + [labels[0]]
        angles = np.linspace(0, 2 * np.pi, n, endpoint=False)
        angles = np.concatenate((angles, [angles[0]]))

        fig = plt.figure()
        ax = fig.add_subplot(111, polar=True)  # 设置坐标为极坐标

        # 画若干个五边形
        floor = np.floor(centers.min())  # 大于最小值的最大整数
        ceil = np.ceil(centers.max())  # 小于最大值的最小整数
        for i in np.arange(floor, ceil + 0.5, 0.5):
            ax.plot(angles, [i] * (n + 1), '--', lw=0.5, color='black')

        # 画不同客户群的分割线
        for i in range(n):
            ax.plot([angles[i], angles[i]], [floor, ceil], '--', lw=0.5, color='black')

        # 画不同的客户群所占的大小
        kinds = kinds.values

        print(kinds.size, centers.shape)
        for i in range(len(kinds)):
            # print(centers[i], kinds[i])
            ax.plot(angles, centers[i], lw=2, label=kinds[i])
            ax.fill(angles, centers[i], facecolor="g", alpha=0.25)
            # ax.fill(angles, centers[i])

        ax.set_thetagrids(angles * 180 / np.pi, labels)  # 设置显示的角度，将弧度转换为角度
        # plt.legend(loc='lower right', bbox_to_anchor=(-2.0, 0.0))  # 设置图例的位置，在画布外
        plt.title(kinds[-1])
        ax.set_theta_zero_location('N')  # 设置极坐标的起点（即0°）在正北方向，即相当于坐标轴逆时针旋转90°
        ax.spines['polar'].set_visible(False)  # 不显示极坐标最外圈的圆
        ax.grid(True)  # 不显示默认的分割线
        ax.set_yticks([])  # 不显示坐标间隔
        plt.savefig(f'E:\\pycharm\\PyCharm 2021.1.1\\test\picture\\{data.iloc[0, -1]}.png')


#
# print(ddf.shape)
# [Graph().plot_ra
print(df['性能评分'].head(11))
Graph().bar(x='名称', y='性能评分', data=df)
# dar(ddf.iloc[i:i+1, :]) for i in range(ddf.shape[0])]



