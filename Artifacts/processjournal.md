I wanted to initially make a point and click car mover as idle management games are always super fun to me, I thought i could build an industry managment type game where you need to select cars and send them to the correct destination in order to make the most money. you have to send them on the correct route, as if your cars collide you lose. I was inspired by games like mini motorways.  

![Alt text](<Screenshot 2023-10-19 150140.png>)

I got to work importing a lot of free low poly assets I found from [Miziziziz Github](https://github.com/Miziziziz/Retro3DGraphicsCollection) 

it is hard becuase there was no regulation among asset packs so they all had their own caveats when importing into unity. 
in the end I got a scene setup.

![Alt text](<Screenshot 2023-10-19 150732.png>)

the trees were the most work as there were 36 of them, and for all of them I had to manually click and drag the correct texture onto the albedo of the material. They all also had to be set to cutout instead of opaque in addition to some other texture settings. otherwise the trees would be black boxes. 
![Alt text](<Screenshot 2023-10-19 150753.png>)

I decided the most important part of this was setting up a good way to let the player know what they can interact with.
I followed [this tutorial](https://www.youtube.com/watch?v=Bm6Bmcjd1Mw) to create a shader that draws an outline around the object that can be interacted with.

https://github.com/IAMColumbia/gp2portfoliogame-chopov1/assets/76492881/db8f97aa-fa19-4d62-9fb7-06318d191c8f

I spent 4 hours working on a node traversal system so the car can go from on place to another on the road. In the video the red node is the node the car wants to get to ultimatly, the blue/yellow node is the next closest node the vehicle can get to. this did not work at all as you can see in the video. 

https://github.com/IAMColumbia/gp2portfoliogame-chopov1/assets/76492881/06e8e2e9-7842-446c-8156-6a9fc64efeda

I am debating doing an a star implementation for this, and getting raycasts to the gorund material and checking if its drivable. This would require me to create a grid of nodes rather then a path, i also feel like there are many caveats with making this look smooth unless I had grid based assets. Mini motorways is a 2d grid based game, so that implementation might make sense for that game. 

## Thursday October 26, 2023

Working on creating an interactable system for my game studio game. I have been having a very hard time figuring out how to create a reusable puzzle piece in Unreal. I think it is because I am trying to do stuff the Unity way. In Untiy I would create a prefab with an interactable script, and create a unity event that can trigger whatever we want to have happen when the player interacts with it.

https://github.com/IAMColumbia/ApocolypseTrain/assets/76492881/4dce31d2-6309-491f-b2d9-356414a0836a

![Alt text](<Screenshot 2023-10-26 155400.png>)

I am not sure how to achieve this exact pattern in unreal. I had 2 ideas to get started.

Use a ray cast to check if the actor has an intractable component on it.  If a is pressed and the ray cast is colliding with the object then activate the component. 

Or we have a component that talks with the player manager subsystem and if any players are overlapping then render depth is set to true else false. 

After building this system out in unity and seeing it working, and seeing the limited time I had left before the end of the week I decided to do it the hacky way and get it to work by just using a method inside the train class. I felt for the scale of game I was making it wouldnt be too much of a burden. 

## Saturday October 28, 2023  

I decided to turn things around on my node based vehicle AI. Since I am building a train game in game studio, and trains have a set path unlike cars, I decided to turn my vehicle managment sim into a train management sim as this felt much more doable. I re-worked the code to achieve a train that follows a path.

https://github.com/IAMColumbia/ApocolypseTrain/assets/76492881/8afa7508-8aa8-4b99-809a-0e156e26fa7f

I decided next I wanted to make the train move along a designated path, and that I also wanted to procedurally spawn in track segments between the nodes I created. I worked with chatGPT for about 2 hours and I gotsome kind of result.

![Alt text](<Screenshot 2023-10-28 174125.png>)

But I could never get it to work with curves

![Alt text](<Screenshot 2023-10-28 174242.png>)

I found a [video](https://www.youtube.com/watch?v=ViVVgjqf2XA) of a guy showing how he setup a train path system with cinemachine using their path scripts.

I thought this was really clever, and if I was going to try to build this in unreal I would try to use splines as they are similar to cinemachines paths. With this in mind, ill attempt the cinemachine method. Luckily the person who created the video also had a unitypackage I was able to install and get working out of the box

https://github.com/IAMColumbia/ApocolypseTrain/assets/76492881/1cb8c250-70e0-4b52-89d9-61e34e322425

## Sunday October 29, 2023

I decided to create an extremely basic economy system with the cinemachine train carts as I knew I was not going to get the full idea with different resources and train lines setup in time. I created a train station trigger, which I originally had in the back, but I moved to front for readability as its kindof the most important part of the game. I added the ability to add trains onto the track. Initially I had hoped to be able to attach them to the same train, but I could not get this to work in a reasonable amount of time so I abandoned the idea. I added basic UI elements and made a fun pop up for when the trains go past the station and the players money increases.

https://github.com/IAMColumbia/ApocolypseTrain/assets/76492881/2644183f-0400-4a25-8870-e67d3b5e1492


Here is a video of breaking the game. There are no longer any meaningful choices left to be made because no matter what button you click you will barely see a difference in profits.

https://github.com/IAMColumbia/ApocolypseTrain/assets/76492881/4ce3848d-21da-4f1e-a39f-5d27d02edbe5