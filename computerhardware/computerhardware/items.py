# Define here the models for your scraped items
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/items.html

import scrapy


class CPU(scrapy.Item):
    # define the fields for your item here like:
    CPU_name = scrapy.Field()
    CPU_series = scrapy.Field()
    CPU_dominant_frequency = scrapy.Field()
    CPU_price = scrapy.Field()


class MotherBoard(scrapy.Item):
    MB_name = scrapy.Field()
    MB_price = scrapy.Field()
    MB_compatibility = scrapy.Field()
    MB_chip_suite = scrapy.Field()
    MB_hd_compatibility = scrapy.Field()
    MB_my_compatibility = scrapy.Field()


class Memory(scrapy.Item):
    MY_name = scrapy.Field()
    MY_type = scrapy.Field()
    MY_price = scrapy.Field()
    MY_frequency = scrapy.Field()
    MY_capacity = scrapy.Field()


class KeyBoard(scrapy.Item):
    KB_name = scrapy.Field()
    KB_type = scrapy.Field()
    KB_price = scrapy.Field()

class HardDrive(scrapy.Item):
    HD_name = scrapy.Field()
    HD_type = scrapy.Field()
    HD_price = scrapy.Field()
    HD_capacity = scrapy.Field()

class Mice(scrapy.Item):
    MC_name = scrapy.Field()
    MC_frequency = scrapy.Field()
    MC_price = scrapy.Field()
    MC_resolution_ratio = scrapy.Field()


class LCD(scrapy.Item):
    LCD_name = scrapy.Field()
    LCD_resolution_ratio = scrapy.Field()
    LCD_price = scrapy.Field()
    LCD_scale = scrapy.Field()


class Case(scrapy.Item):
    Case_name = scrapy.Field()
    Case_price = scrapy.Field()
    Case_structure = scrapy.Field()

class DVDrw(scrapy.Item):
    DR_name = scrapy.Field()
    DR_price = scrapy.Field()
    DR_type = scrapy.Field()


class VGA(scrapy.Item):
    VGA_name = scrapy.Field()
    VGA_brand = scrapy.Field()
    VGA_price = scrapy.Field()
    VGA_chip = scrapy.Field()
    VGA_series = scrapy.Field()
    VGA_capacity = scrapy.Field()