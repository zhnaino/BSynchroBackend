﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class TransactionRequest
    {
        public int AccountId { get; set; }
        public double Deposit { get; set; }
    }
}
