using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class BookBadRequstException : BadRequestException
    {
        public BookBadRequstException(Guid bookId) : base($"Book with id: {bookId} does not exist")
        {

        }
    }
}
