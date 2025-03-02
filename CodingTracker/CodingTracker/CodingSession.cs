using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
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

                // Duration calculation needed below to ensure Duration receives a value when parameterless (Dapper) constructor is invoked
                Duration = CalculateDuration();
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

                // Duration calculation needed below to ensure Duration receives a value when parameterless (Dapper) constructor is invoked
                Duration = CalculateDuration();
            }
        }

        public TimeSpan? Duration { get; private set; }

        //////////////////////////////////////////// CONSTRUCTORS ///////////////////////////////////
        public CodingSession() // Parameterless query needed for Dapper
        {
            // Avoid uninitialized fields -- The DateTime.MinValue here never actually gets added to the database
            // -- This is a safety measure to avoid potential "Use of unassigned local variable (CS0165)" compiler errors
            _startTime = DateTime.MinValue; 
        }

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

