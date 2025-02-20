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
        private DateTime _endTime;

        /////////////////////////////////////////// PROPERTIES //////////////////////////
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_endTime != default && value > _endTime)
                {
                    throw new ArgumentException("Start time cannot be after end time.");
                }

                _startTime = value;
            }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                // Input validation goes here

                _endTime = value;
            }
        }

        public int Id
        { get; private set; } // Use private set since the database auto-increments Id


        public TimeSpan Duration => CalculateDuration();

        //////////////////////////////////////////// CONSTRUCTORS ///////////////////////////////////
        public CodingSession(DateTime startTime)
        {
            _startTime = startTime;
        }

        public CodingSession(DateTime startTime, DateTime endTime) // Include overloaded constructor for flexibility -- so user can specify an end time right away
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        /// ///////////////////////////////////// METHODS ///////////////////////////////////

        public TimeSpan CalculateDuration()
        {
            if (StartTime == default || EndTime == default)
                throw new InvalidOperationException("Both Start time and End time must be set");

            TimeSpan duration = TimeSpan.FromSeconds(Math.Round((EndTime - StartTime).TotalSeconds));
            return duration;

        }

    }
}
