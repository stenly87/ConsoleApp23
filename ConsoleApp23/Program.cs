﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp23
{
    class Program
    {
        static void Main(string[] args)
        {
            // сериализация
            // существует несколько типов данных, в которые можно
            // сериализовать объекты (и десериализовать)

            // для выбора типа данных используются форматтеры:
            // namespace System.Runtime.Serialization.Formatters.Binary.
            BinaryFormatter bin = new BinaryFormatter();
            //bin.Serialize(ссылка_на_поток, obj);
            //bin.Deserialize(ссылка_на_поток);
            string text = "сохраняемый текст";
            using (var fs = File.Create("test.bin"))
            {
                bin.Serialize(fs, text);
            }

            using (var fs = File.OpenRead("test.bin"))
            {
                string result = (string)bin.Deserialize(fs);
                Console.WriteLine(result);
            }
            // формат xml - текстовый формат, все значения в тегах
            // при создании XmlSerializer ему указывается тип данных
            // с которым он будет работать
            // namespace System.Xml.Serialization
            XmlSerializer xmlSerializer = 
                new XmlSerializer(typeof(string));
            using (var fs = File.Create("test.xml"))
            {
                xmlSerializer.Serialize(fs, text);
            }
            using (var fs = File.OpenRead("test.xml"))
            {
                string result = (string)xmlSerializer.Deserialize(fs);
                Console.WriteLine(result);
            }

            // json [
            // {"name1": 1,"name2": "value2"},
            // {"name1": 2,"name2": "value2"}
            // ]
            // namespace System.Runtime.Serialization.Json
            DataContractJsonSerializer jsonSerializer =
                new DataContractJsonSerializer(typeof(string));
            using (var fs = File.Create("test.json"))
            {
                jsonSerializer.WriteObject(fs, text);
            }
            using (var fs = File.OpenRead("test.json"))
            {
                string result = (string)jsonSerializer.ReadObject(fs);
                Console.WriteLine(result);
            }

            // json и xml сохраняют только публичные данные объектов
            // binaryformatter сохраняет и приватные данные
            // сериализации подвержены только поля (свойства)
        }
    }
}
