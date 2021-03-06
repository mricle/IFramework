﻿using Sample.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IFramework.Command;
using IFramework.AspNet;
using System.Web;
using IFramework.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections;

namespace Sample.CommandHttpClient
{
    public class ArrayModelCollection
    {
        public ArrayModel[] ArrayModels { get; set; }
    }
    public class ArrayModel
    {
        public ArrayModel()
        {
            DateTime = DateTime.Now;
        }
        public string[] Ids { get; set; }
        public DateTime DateTime { get; set; }
        public ArrayModel[] ArrayModels { get; set; }
    }

    public class ModifyCooperatorBasic : ICommand
    {
        public string CooperatorID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Remark { get; set; }

        public bool NeedRetry
        {
            get; set;
        }

        public string ID
        {
            get; set;
        }

        public ModifyCooperatorBasic() { }

    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpClientGetTest();
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
            var apiClient = new HttpClient();
            // apiClient.BaseAddress = new System.Uri("http://localhost:6357");
            apiClient.BaseAddress = new System.Uri("http://localhost:63581");

            var command = new ModifyCooperatorBasic
            {
                CooperatorID = "52b007cd9a37601480b6d5e6",
                Name = "ivan",
                Type = 0,
                Remark = ""
            };

            var task = apiClient.DoCommand(command);

            var result = task.Result.Content.ReadAsStringAsync().Result;
            Console.Write(result);
        }

        public static void HttpClientGetTest()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:63581")
            };
            var model1 = new ArrayModel { Ids = new string[] { "1", "3", "5" } };
            var model2 = new ArrayModel { Ids = new string[] { "2", "4", "6" } };
            var model3 = new ArrayModel { Ids = new string[] { "a", "b", "c" } };

            //var json = JsonConvert.SerializeObject(model);
            //var nameValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            //var queryString = HttpUtility.ParseQueryString("");

            //var nameValueCollection = ToNameValueCollection(model);


            //Console.Write(nameValueCollection.ToString());

            model2.ArrayModels = new ArrayModel[] { model3 };
            var arrayModelCollection = new ArrayModelCollection
            {
                ArrayModels = new ArrayModel[] { model1, model2 }
            };
          //  var result1 = client.GetAsync<ArrayModel>("api/BatchCommand", model1).Result;

            var result2 = client.GetAsync<ArrayModelCollection>("api/BatchCommand/Collection", arrayModelCollection).Result;
        }
    }
}
