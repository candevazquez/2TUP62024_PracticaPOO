﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Web.Ejr2;



namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class Ejr2Controller : Controller
    {
        // Lista estática para almacenar los álbumes
        private static List<PhotoBook> photoBooks = new List<PhotoBook>();
        private static int currentId = 1;

        // POST: api/PhotoBook/CreateStandard
        [HttpPost("CreateStandard")]
        public IActionResult CreateStandardPhotoBook([FromQuery] int? numPages)
        {
            // Crear un álbum con el número de páginas especificado o por defecto 16
            PhotoBook newPhotoBook = numPages.HasValue ? new PhotoBook(numPages.Value) : new PhotoBook();
            photoBooks.Add(newPhotoBook);
            return Ok(new { Id = currentId++, Pages = newPhotoBook.GetNumberPages() });
        }

        // POST: api/PhotoBook/CreateBig
        [HttpPost("CreateBig")]
        public IActionResult CreateBigPhotoBook()
        {
            BigPhotoBook newBigPhotoBook = new BigPhotoBook();
            photoBooks.Add(newBigPhotoBook);
            return Ok(new { Id = currentId++, Pages = newBigPhotoBook.GetNumberPages() });
        }

        // GET: api/PhotoBook/GetPages/5
        [HttpGet("GetPages/{id}")]
        public IActionResult GetNumberOfPages(int id)
        {
            var photoBook = photoBooks.ElementAtOrDefault(id - 1);
            if (photoBook == null)
            {
                return NotFound("Álbum no encontrado");
            }

            return Ok(photoBook.GetNumberPages());
        }

        // GET: api/PhotoBook/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllPhotoBooks()
        {
            return Ok(photoBooks.Select((book, index) => new { Id = index + 1, Pages = book.GetNumberPages() }));
        }
    }
}

//Ej2
//Creación de Clases:

//Implementar una clase PhotoBook con un atributo protected llamado numPages de tipo int.
//Esta clase debe tener un método público GetNumberPages que retorne el número de páginas del álbum.
//El constructor por defecto debe inicializar el álbum con 16 páginas.
//Incluir un constructor que permita especificar el número de páginas del álbum.
//Crear una clase derivada BigPhotoBook cuyo constructor inicialice el álbum con 64 páginas.