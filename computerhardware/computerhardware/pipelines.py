# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html


# useful for handling different item types with a single interface
from itemadapter import ItemAdapter
import pymongo


class MongoCPUPipLine:
    def __init__(self):
        self.MyClient = pymongo.MongoClient('localhost')
        self.translation: dict = {'cpu': 'CPU_name', 'motherboard': 'MB_name',
                                  'memory': 'MY_name', 'keyboard': 'KB_name',
                                  'hard_drive': 'HD_name', 'mice': 'MC_name',
                                  'lcd': 'LCD_name', 'case': "Case_name",
                                  'dvdrw': 'DR_name', 'vga':'VGA_name'}
        self.my_db = self.MyClient['computer']

    def process_item(self, item, spider):

        if category := self.translation.get(spider.name):
            col: str = category.split('_')[0].lower()
            my_col = self.my_db[col]
            if my_col.find({category: item.get(category)}).count():
                my_col.replace_one({category: item.get(category)}, dict(item))
            else:
                my_col.insert_one(dict(item))
            return item

