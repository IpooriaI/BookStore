﻿namespace BookStore.Infrastructure.Application
{
    public interface UnitOfWork
    {
        void Commit();
    }
}
