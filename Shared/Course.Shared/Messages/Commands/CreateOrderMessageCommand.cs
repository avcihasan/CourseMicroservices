﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.Messages.Commands
{
    public class CreateOrderMessageCommand
    {
        public CreateOrderMessageCommand()
        {
            OrderItems = new List<OrderItem>();
        }
        public string BuyerId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string Line { get; set; }
    }

    public class OrderItem
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
