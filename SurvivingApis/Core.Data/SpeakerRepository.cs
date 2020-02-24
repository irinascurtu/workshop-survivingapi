using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Helpers;
using Core.Domain;

namespace Core.Data
{
    public interface ISpeakerRepository
    {
        void AddSpeaker(Speaker speaker);
        bool SpeakerExists(int speakerId);
        void DeleteSpeaker(Speaker speaker);
        Speaker GetSpeaker(int speakerId);
        IEnumerable<Speaker> GetSpeakers();
        IEnumerable<Speaker> GetSpeakers(IEnumerable<int> speakerIds);
        void UpdateSpeaker(Speaker speaker);
        bool Save();
        PagedList<Speaker> GetSpeakers(SpeakerResourceParameters speakerResourceParameters);
    }

    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ConferenceContext _context;

        public SpeakerRepository(ConferenceContext context)
        {
            _context = context;
        }


        public void AddSpeaker(Speaker speaker)
        {
            if (speaker == null)
            {
                throw new ArgumentNullException(nameof(speaker));
            }

            // the repository fills the id (instead of using identity columns)
            speaker.Id = new int();

            foreach (var talk in speaker.Talks)
            {
                talk.Id = new int();
            }

            _context.Speakers.Add(speaker);
        }

        public bool SpeakerExists(int speakerId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            return _context.Speakers.Any(a => a.Id == speakerId);
        }

        public void DeleteSpeaker(Speaker speaker)
        {
            if (speaker == null)
            {
                throw new ArgumentNullException(nameof(speaker));
            }

            _context.Speakers.Remove(speaker);
        }

        public Speaker GetSpeaker(int speakerId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            return _context.Speakers.FirstOrDefault(a => a.Id == speakerId);
        }

        public IEnumerable<Speaker> GetSpeakers()
        {
            return _context.Speakers.ToList<Speaker>();
        }

        public PagedList<Speaker> GetSpeakers(SpeakerResourceParameters speakerResourceParameters)
        {
            if (speakerResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(speakerResourceParameters));
            }

            var collection = _context.Speakers as IQueryable<Speaker>;


            return PagedList<Speaker>.Create(collection,
                speakerResourceParameters.PageNumber,
                speakerResourceParameters.PageSize);
        }

        public IEnumerable<Speaker> GetSpeakers(IEnumerable<int> speakerIds)
        {
            if (speakerIds == null)
            {
                throw new ArgumentNullException(nameof(speakerIds));
            }

            return _context.Speakers.Where(a => speakerIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateSpeaker(Speaker speaker)
        {
            // no code in this implementation
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
