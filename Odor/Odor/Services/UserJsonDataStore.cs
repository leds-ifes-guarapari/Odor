﻿using Odor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Odor.Services.UserJsonDataStore))]
namespace Odor.Services
{
    public class UserJsonDataStore : IDataStore<User>
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "user.json");

        private DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User));

        private User user;

        public UserJsonDataStore()
        {
            this.user = serializer.ReadObject(File.Open(path, FileMode.OpenOrCreate)) as User;
        }

        public Task<bool> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(User user)
        {
            return Task.FromResult(this.user);
        }

        public Task<IEnumerable<User>> On(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user)
        {
            this.user = user;
            serializer.WriteObject(File.OpenWrite(path), this.user);
            return Task.FromResult(true);
        }
    }
}