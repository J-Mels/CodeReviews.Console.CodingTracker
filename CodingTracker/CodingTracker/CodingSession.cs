using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class CodingSession
    {
        ////////////////////////////////////////////// FIELDS ///////////////////////////////
        private DateTime _startTime;
        private DateTime? _endTime;

        /////////////////////////////////////////// PROPERTIES //////////////////////////
        public int Id { get; private set; }

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_endTime.HasValue && value > _endTime.Value)
                {
                    throw new ArgumentException("Start time cannot be after end time.");
                }
                _startTime = value;
            }
        }

        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                if (value.HasValue && value < _startTime)
                {
                    throw new ArgumentException("End time cannot be before start time.");
                }
                _endTime = value;
            }
        }

        public TimeSpan? Duration { get; private set; }

        //////////////////////////////////////////// CONSTRUCTORS ///////////////////////////////////
        public CodingSession(DateTime startTime, DateTime? endTime = null)
        {
            _startTime = startTime;
            EndTime = endTime;  // Use the property setter for validation
            Duration = CalculateDuration();
        }

        //////////////////////////////////////////// METHODS ///////////////////////////////////

        private TimeSpan? CalculateDuration()
        {
            return EndTime.HasValue ? EndTime - StartTime : null;
        }
    }
}

