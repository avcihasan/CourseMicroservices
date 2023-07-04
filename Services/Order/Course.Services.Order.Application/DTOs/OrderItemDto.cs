using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.DTOs
{
    public class OrderItemDto
    {
        public string CourseId { get;  set; }
        public string CourseName { get;  set; }
        public string PictureUrl { get;  set; }
        public Decimal Price { get;  set; }
    }
}
