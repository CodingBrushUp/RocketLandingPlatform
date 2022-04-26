# RocketLandingPlatform
Design a library that will help determine if rockets can land on a platform. 
Code-along to build a full-featured management dashboard from scratch


    Full-stack web application development skills
    Whenever a rocket is getting back from orbit, it needs to check every now and then if it's on a correct trajectory to safely land on a platform. 
    The whole landing area (the area that contains the landing platform and surroundings) consists of multiple squares that set a perimeter/dimensions that can be described with coordinates (say x and y). 
    
    
    
 Assuming that the landing area has a size of square 100x100 and the landing platform has a size of a square 10x10 and its top left corner starts at a position 5,5 (please assume that position 0,0 is located at the top left corner of the landing area and all positions are relative to it), the library should work as follows: 
 if rocket asks for position 5,5 it replies `ok for landing`
 if rocket asks for position 16,15, it replies `out of platform`
 if the rocket asks for a position that has previously been checked by another rocket (only the last check counts), it replies with `clash`
 if the rocket asks for a position that is located next to a position that has previously been checked by another rocket (say, the previous rocket asked for position 7,7 and the rocket asks for 7,8 or 6,7 or 6,6), it replies with `clash`. Given the above.
	
Please create a library (just a library, it doesn't need to be used on any client/GUI) that will support the following features:
	## rocket can query it to see if it's on a good trajectory to land at any moment
	## library can return one of the following values: 'out of platform', 'clash', 'ok for landing'
more than one rocket can land on the same platform at the same time and rockets need to have at least one unit separation between their landing positions!
