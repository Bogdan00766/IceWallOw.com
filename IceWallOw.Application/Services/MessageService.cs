using AutoMapper;
using Domain.IRepositories;
using Domain.Models;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceWallOw.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }
        public async Task<List<MessageDto>> GetAllChatMessages(ChatDto chat)
        {
            var list = await _messageRepository.FindByIdAsync(chat.Id);
            return _mapper.Map<List<MessageDto>>(list);
        }

        public async Task<MessageDto> Send(ChatDto chat, string content)
        {
            Message message = new Message();
            message.Content = content;
            message.Date = DateTime.Now;
            message = _messageRepository.Create(message);
            var cht = await _chatRepository.FindByIdAsync(chat.Id);
            cht.Messages.Add(message);
            _chatRepository.SaveAsync();

            return _mapper.Map<MessageDto>(message);
        }
    }
}
