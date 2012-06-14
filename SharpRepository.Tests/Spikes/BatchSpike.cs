﻿using System;
using System.Linq;
using NUnit.Framework;
using SharpRepository.Repository;
using SharpRepository.Tests.TestObjects;
using Should;

namespace SharpRepository.Tests.Spikes
{
    [TestFixture]
    public class BatchSpike: TestBase
    {
        [Test]
        public void Repository_Should_BeginBatch()
        {
            var repository = new InMemoryRepository<Contact, Int32>();
            
            using (var batch = repository.BeginBatch())
            {
                batch.Add(new Contact { Name = "Test User 1" });

                var result = repository.GetAll();
                result.Count().ShouldEqual(0); // shouldn't have really been added yet

                batch.Add(new Contact { Name = "Test User 2" });

                result = repository.GetAll();
                result.Count().ShouldEqual(0); // shouldn't have really been added yet

                batch.Commit();
            }

            repository.GetAll().Count().ShouldEqual(2);
        }
    }
}