using AutoMapper;
using MyEshopDemo.Models;
using MyEshopDemo.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyEshopDemo.Controllers.Api
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/messages
        public IHttpActionResult GetMessages()
        {
            var messagesDtos = db.Messages
                .ToList()
                .Select(Mapper.Map<Message, MessageDto>);
            return Ok(messagesDtos);
        }

        // GET api/messages/1
        public IHttpActionResult GetMessage(int id)
        {
            var messageInDb = db.Messages.SingleOrDefault(m => m.Id == id);
            if (messageInDb == null)
                return NotFound();

            return (Ok(Mapper.Map<Message, MessageDto>(messageInDb)));
        }

        // POST api/messages
        [HttpPost]
        public IHttpActionResult CreateMessage(MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var message = Mapper.Map<MessageDto, Message>(messageDto);
            message.Date = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();

            messageDto.Id = message.Id;

            return Created(new Uri(Request.RequestUri + "/" + message.Id), messageDto);
        }

    }
}
