using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Test.Queue
{
    public class QueueTest : IDisposable
    {
        private List<IQueue<int>> queues = new List<IQueue<int>>();

        public QueueTest()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
