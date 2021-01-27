# Organized-MGSV-Music-Shuffler

Install notes: 

	Place MGSV Music Shuffler.exe in your game folder, run it, good to go.  
_____________________________

Description:

	This application allows you to shuffle your custom music, but keep certain things unshuffled with tags.  You can specify a helicopter song so that will never be shuffled and can persist through shuffles.  You can also store specific orderings to make impromptu internalized "playlists".  

	All tags should be wrapped in two ##'s and can be placed at the start of the filename.  

	Supported tags:
		#H# - Helicopter song
		#S# - Save song position

	Example for marking a helicopter song: 
		000 #H# Blank Banshee - Bathsalts.mp3
	
	
	Helicopter song will always be first and will not get randomized.  You can set this song in game and be confident it won't change when you shuffle.  

	Example for marking songs to keep their current position: 
		002 #S# aliceffekt - Vermillionth - 02 The Wises Were Wrong.mp3
		003 #S# aliceffekt - Vermillionth - 03 Millionth Nebulum Compressor.mp3
		004 #S# aliceffekt - Vermillionth - 04 Today Was Amazing, Tomorrow Not So Much.mp3


	These songs will always be 2nd, 3rd, and 4th in the list.  You can use this to keep your songs bunched together as a makeshift "playlist" while randomizing the rest.
