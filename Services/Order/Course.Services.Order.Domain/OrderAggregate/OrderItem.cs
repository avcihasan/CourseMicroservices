using Course.Services.Order.Domain.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:BaseEntity
    {
        public string CourseId { get; private set; }
        public string CourseName { get; private set; }
        public string PictureUrl { get; private set; }
        public Decimal Price { get; private set; }
        public OrderItem()
        {
            
        }
        public OrderItem(string courseId, string courseName, string pictureUrl, decimal price)
        {
            CourseId = courseId;
            CourseName = courseName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public void UpdateOrderItem(string courseName, string pictureUrl, decimal price)
        {
            CourseName = courseName;
            Price = price;
            PictureUrl = pictureUrl;
        }
    }
}
