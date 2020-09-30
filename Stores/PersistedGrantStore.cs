using IdentityServer.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Stores
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        readonly ApplicationDbContext _context;
        public PersistedGrantStore(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            Task<IEnumerable<PersistedGrant>> task = Task<IEnumerable<PersistedGrant>>.Factory.StartNew(() =>
            {
                return _context.PersistedGrants.Where(p => p.SubjectId == subjectId).ToList();
            });

            return await task;
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            Task<PersistedGrant> task = Task<PersistedGrant>.Factory.StartNew(() =>
            {
                return _context.PersistedGrants.SingleOrDefault(p => p.Key == key);
            });

            return await task;
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                var grants = _context.PersistedGrants.Where(p => p.SubjectId == subjectId && p.ClientId == clientId);
                _context.PersistedGrants.RemoveRange(grants);
            });

            return task;
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                var grants = _context.PersistedGrants.Where(p => p.SubjectId == subjectId && p.ClientId == clientId && p.Type == type);
                _context.PersistedGrants.RemoveRange(grants);
            });

            return task;
        }

        public Task RemoveAsync(string key)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                var grants = _context.PersistedGrants.Where(p => p.Key == key);
                _context.PersistedGrants.RemoveRange(grants);
            });

            return task;
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    _context.PersistedGrants.AddAsync(grant);
                    _context.SaveChanges();
                });

                return task;
            }
        }
    }
}
