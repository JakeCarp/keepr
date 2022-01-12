using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        internal List<Keep> GetAllKeeps()
        {
            return _repo.GetAllKeeps();
        }

        internal Keep GetKeepById(int id)
        {
            var keep = _repo.GetKeepById(id);
            if (keep == null)
            {
                throw new Exception("Bad Keep Id");
            }
            var likedKeep = LikeKeep(keep);
            return likedKeep;
        }

        internal Keep UpdateKeep(string id, Keep update)
        {
            var keep = isKeepOwner(id, update.Id);
            keep.Name = update.Name ?? keep.Name;
            keep.Img = update.Img ?? keep.Img;
            keep.Description = update.Description ?? keep.Description;
            return _repo.Update(keep);
        }

        internal List<Keep> GetKeepsByUserId(string id)
        {
            return _repo.GetKeepsByUserId(id);
        }

        internal void incrementKeeps(int keepId)
        {
            Keep keep = GetKeepById(keepId);
            keep.Keeps++;
            _repo.ChangeKeeps(keep);
        }

        internal string DeleteKeep(string userId, int keepId)
        {
            var keep = isKeepOwner(userId, keepId);
            _repo.Delete(keepId);
            return "Keep Deleted";
        }

        internal void decrementKeeps(int keepId)
        {
            Keep keep = GetKeepById(keepId);
            keep.Keeps--;
            _repo.ChangeKeeps(keep);
        }

        internal Keep isKeepOwner(string userId, int keepId)
        {
            var keep = GetKeepById(keepId);
            if (keep.CreatorId != userId)
            {
                throw new Exception("This is not your keep!");
            }
            return keep;
        }
        internal Keep LikeKeep(Keep keep)
        {
            keep.Views++;
            _repo.ChangeViews(keep);
            return keep;
        }


    }
}