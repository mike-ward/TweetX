﻿using Loon.Interfaces;
using Loon.Services;
using Twitter.Models;

namespace Loon.Commands
{
    public class SetUserProfileContextCommand : BaseCommand
    {
        private readonly ITwitterService twitterService;

        public SetUserProfileContextCommand(ITwitterService twitterService)
        {
            this.twitterService = twitterService;
        }

        public override void Execute(object? parameter)
        {
            PubSubs.SetUserProfileContext.Publish(parameter as User);
        }
    }
}