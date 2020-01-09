namespace ConferencePlanner.DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var loader = new DevIntersectionLoader();

            using var context = new ApplicationDbContext();
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("ConferencePlanner.DataLoader.DevIntersection_Vegas_2017.json");

            loader.LoadDataAsync(stream, context).Wait();
        }
    }

    public class DevIntersectionLoader
    {
        public async Task LoadDataAsync(Stream fileStream, ApplicationDbContext db)
        {
            var reader = new JsonTextReader(new StreamReader(fileStream));

            var speakerNames = new Dictionary<string, Speaker>();
            var tracks = new Dictionary<string, Track>();

            var doc = await JArray.LoadAsync(reader);

            foreach (var jToken in doc)
            {
                var item = (JObject) jToken;
                var theseSpeakers = new List<Speaker>();
                foreach (var thisSpeakerName in item["speakerNames"])
                {
                    if (!speakerNames.ContainsKey(thisSpeakerName.Value<string>()))
                    {
                        var thisSpeaker = new Speaker {FullName = thisSpeakerName.Value<string>()};
                        db.Speakers.Add(thisSpeaker);
                        speakerNames.Add(thisSpeakerName.Value<string>(), thisSpeaker);
                        Console.WriteLine(thisSpeakerName.Value<string>());
                    }

                    theseSpeakers.Add(speakerNames[thisSpeakerName.Value<string>()]);
                }

                var theseTracks = new List<Track>();
                foreach (var thisTrackName in item["trackNames"])
                {
                    if (!tracks.ContainsKey(thisTrackName.Value<string>()))
                    {
                        var thisTrack = new Track {Name = thisTrackName.Value<string>()};
                        db.Tracks.Add(thisTrack);
                        tracks.Add(thisTrackName.Value<string>(), thisTrack);
                    }

                    theseTracks.Add(tracks[thisTrackName.Value<string>()]);
                }

                var session = new Session
                {
                    Title = item["title"].Value<string>(),
                    StartTime = item["startTime"].Value<DateTime>(),
                    EndTime = item["endTime"].Value<DateTime>(),
                    Track = theseTracks[0],
                    Abstract = item["abstract"].Value<string>(),
                    SessionSpeakers = new List<SessionSpeaker>()
                };

                foreach (var sp in theseSpeakers)
                    session.SessionSpeakers.Add(new SessionSpeaker
                    {
                        Session = session,
                        Speaker = sp
                    });

                db.Sessions.Add(session);

                db.SaveChanges();
            }
        }
    }
}
