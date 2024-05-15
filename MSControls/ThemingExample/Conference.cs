using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Theming
{
	public class Conference : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string lecturer;
		private string title;
		private string description;

		public string Lecturer
		{
			get { return this.lecturer; }
			set
			{
				if (value != this.lecturer)
				{
					this.lecturer = value;
					this.OnPropertyChanged("lecturer");
				}
			}
		}

		public string Title
		{
			get { return this.title; }
			set
			{
				if (value != this.title)
				{
					this.lecturer = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		public string Description
		{
			get { return this.description; }
			set
			{
				if (value != this.title)
				{
					this.lecturer = value;
					this.OnPropertyChanged("Description");
				}
			}
		}


		public Conference()
		{

		}

		public Conference(string lecturer, string title, string description)
		{
			this.lecturer = lecturer;
			this.title = title;
			this.description = description;

		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, args);
			}
		}

		private void OnPropertyChanged(string propertylecturer)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertylecturer));
		}

		public override string ToString()
		{
			return this.lecturer;
		}

		public static ObservableCollection<Conference> GetConferences()
		{
			ObservableCollection<Conference> Conferences = new ObservableCollection<Conference>();
			Conference p;

			p = new Conference("Ray Kurzweil", "Get ready for hybrid thinking", "Two hundred million years ago, our mammal ancestors developed a new brain feature: the neocortex. This stamp-sized piece of tissue (wrapped around a brain the size of a walnut) is the key to what humanity has become. Now, futurist Ray Kurzweil suggests, we should get ready for the next big leap in brain power, as we tap into the computing power in the cloud.");
			Conferences.Add(p);
			p = new Conference("Lorrie Faith Cranor", "What’s wrong with your pa$$w0rd?", "Lorrie Faith Cranor studied thousands of real passwords to figure out the surprising, very common mistakes that users -- and secured sites -- make to compromise security. And how, you may ask, did she study thousands of real passwords without compromising the security of any users? That's a story in itself. It's secret data worth knowing, especi...");
			Conferences.Add(p);
			p = new Conference("Chris Domas", "The 1s and 0s behind cyber warfare", @"Chris Domas is a cybersecurity researcher, operating on what’s become a new front of war, @""cyber."" In this engaging talk, he shows how researchers use pattern recognition and reverse engineering (and pull a few all-nighters) to understand a chunk of binary code whose purpose and contents they don't know. ");
			Conferences.Add(p);
			p = new Conference("Toby Shapshak", "You don't need an app for that", "Are the simplest phones the smartest? While the rest of the world is updating statuses and playing games on smartphones, Africa is developing useful SMS-based solutions to everyday needs, says journalist Toby Shapshak. In this eye-opening talk, Shapshak explores the frontiers of mobile invention in Africa as he asks us to reconsider our preconceived notions of innovation.");
			Conferences.Add(p);
			p = new Conference("Del Harvey", "Protecting privacy at Twitter", "Twitter’s Head of Safety, Del Harvey, shares what she’s learned about protecting users' privacy on a platform where activity grows by the billions every two days. Through her funny and insightful anecdotes, she paints a picture of what it’s like to oversee a community where a one-in-a-million chance is pretty good odds. ");
			Conferences.Add(p);
			p = new Conference("Grégoire Courtine", "The paralyzed rat that walked", "A spinal cord injury can sever the communication between your brain and your body, leading to paralysis. Fresh from his lab, Grégoire Courtine shows a new method -- combining drugs, electrical stimulation and a robot -- that could re-awaken the neural pathways and help the body learn again to move on its own. See how it works, as a paralyzed rat...");
			Conferences.Add(p);
			p = new Conference("Sergey Brin", "Why Google Glass?", "It's not a demo, more of a philosophical argument: Why did Sergey Brin and his team at Google want to build an eye-mounted camera/computer, codelecturerd Glass? Onstage at TED2013, Brin calls for a new way of seeing our relationship with our mobile computers -- not hunched over a screen but meeting the world heads-up.");
			Conferences.Add(p);
			p = new Conference("Avi Reichental", "What’s next in 3D printing", "Just like his beloved grandfather, Avi Reichental is a maker of things. The difference is, now he can use 3D printers to make almost anything, out of almost any material. Reichental tours us through the possibilities of 3D printing, for everything from printed candy to highly custom sneakers.");
			Conferences.Add(p);
			p = new Conference("Eric Berlow and Sean Gourley", "Mapping ideas worth spreading", "What do 24,000 ideas look like? Ecologist Eric Berlow and physicist Sean Gourley apply algorithms to the entire archive of TEDx Talks, taking us on a stimulating visual tour to show how ideas connect globally.");
			Conferences.Add(p);
			p = new Conference("James Lyne", "Everyday cybercrime - and what you can do about it ", "How do you pick up a malicious online virus, the kind of malware that snoops on your data and taps your bank account? Often, it's through simple things you do each day without thinking twice. James Lyne reminds us that it's not only the NSA that's watching us, but ever-more-sophisticated cybercriminals, who exploit both weak code and trusting human nature.");
			Conferences.Add(p);
			p = new Conference("Joel Selanikio", "The surprising seeds of a big-data revolution in healthcare", "Collecting global health data was an imperfect science: Workers tramped through villages to knock on doors and ask questions, wrote the answers on paper forms, then input the data — and from this gappy information, countries would make huge decisions. Data geek Joel Selanikio talks through the sea change in collecting health data in the past decade — starting with the Palm Pilot and Hotmail, and now moving into the cloud. ");
			Conferences.Add(p);
			p = new Conference("Jay Silver", "Hack a banana, make a keyboard! ", "Why can't two slices of pizza be used as a slide clicker? Why shouldn't you make music with ketchup? In this charming talk, inventor Jay Silver talks about the urge to play with the world around you. He shares some of his messiest inventions, and demos MaKey MaKey, a kit for hacking everyday objects. ");
			Conferences.Add(p);
			p = new Conference("Juan Enriquez", "Your online life, permanent as a tattoo ", "What if Andy Warhol had it wrong, and instead of being famous for 15 minutes, we’re only anonymous for that long? In this short talk, Juan Enriquez looks at the surprisingly permanent effects of digital sharing on our Conferenceal privacy. He shares insight from the ancient Greeks to help us deal with our new “digital tattoos.” ");
			Conferences.Add(p);
			p = new Conference("Laura Snyder", "The Philosophical Breakfast Club", "In 1812, four men at Cambridge University met for breakfast. What began as an impassioned meal grew into a new scientific revolution, in which these men — who called themselves “natural philosophers” until they later coined “scientist” — introduced four major principles into scientific inquiry. Historian and philosopher Laura Snyder tells their intriguing story. ");
			Conferences.Add(p);
			p = new Conference("Katherine Kuchenbecker", "The technology of touch", "As we move through the world, we have an innate sense of how things feel — the sensations they produce on our skin and how our bodies orient to them. Can technology leverage this? In this fun, fascinating TED-Ed lesson, learn about the field of haptics, and how it could change everything from the way we shop online to how dentists learn the telltale feel of a cavity.");
			Conferences.Add(p);
			p = new Conference("Elon Musk", "The mind behind Tesla, SpaceX, SolarCity ...", "Entrepreneur Elon Musk is a man with many plans. The founder of PayPal, Tesla Motors and SpaceX sits down with TED curator Chris Anderson to share details about his visionary projects, which include a mass-marketed electric car, a solar energy leasing company and a fully reusable rocket.");
			Conferences.Add(p);
			return Conferences;
		}
	}
}
