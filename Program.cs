using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MusicLINQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //=========================================================
            //Solve all of the prompts below using various LINQ queries
            //=========================================================

            //=========================================================
            //There is only one artist in this collection from Mount 
            //Vernon. What is their name and age?
            //=========================================================
            var foundArtist = from artist in Artists where artist.Hometown == "Mount Vernon" select new{artist.RealName, artist.Age};
            foreach(var person in foundArtist)
            {
                Console.WriteLine(person);
            }
            
            //=========================================================
            //Who is the youngest artist in our collection of artists?
            //=========================================================
            var youngestArtist = Artists.Min(artist => artist.Age + " - " + artist.RealName );

            Console.WriteLine(youngestArtist);
            

            //=========================================================
            //Display all artists with 'William' somewhere in their 
            //real name        
            //=========================================================
            var williamsArtist = from artist in Artists where artist.RealName.Contains("William") select new{artist.RealName};
            foreach(var person in williamsArtist)
            {
                Console.WriteLine(person);
            }

            //=========================================================
            //Display all groups with names less than 8 characters
            //=========================================================
            var smallGroups = from groups in Groups where groups.GroupName.Length < 8 select new{groups.GroupName};
            foreach(var group in smallGroups)
            {
                Console.WriteLine(group);
            }

            //=========================================================
            //Display the 3 oldest artists from Atlanta
            //=========================================================
            // var oldArtist = (from artist in Artists orderby artist.Age select artist).Take(3);
            var oldArtist = Artists.Where(artist => artist.Hometown == "Atlanta").OrderByDescending(artist => artist.Age).Take(3).Select(artist => new{artist.RealName, artist.Age, artist.Hometown});
            foreach(var person in oldArtist)
            {
                Console.WriteLine(person);
            }

            //=========================================================
            //(Optional) Display the Group Name of all groups that have 
            //at least one member not from New York City
            //=========================================================
            var groupNotNY = from groups in Groups join artist in Artists on groups.Id equals artist.GroupId where artist.Hometown != "New York City" group groups by groups.GroupName into newGroup select newGroup.Key;
            foreach(var groupObj in groupNotNY)
            {
                Console.WriteLine(groupObj);
            }
            
            //=========================================================
            //(Optional) Display the artist names of all members of the 
            //group 'Wu-Tang Clan'
            //=========================================================r
            var wuTangClan = from artist in Artists join groups in Groups on artist.GroupId equals groups.Id where groups.GroupName == "Wu-Tang Clan" select new{groups.GroupName, artist.ArtistName};
            foreach(var person in wuTangClan)
            {
                Console.WriteLine(person);
            }
        }
    }
}
