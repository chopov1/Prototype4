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

I spent 4 hours working on a node traversal system so the car can go from on place to another on the road. this did not work at all as you can see in the video. I am debating weather the game I had in mind is doable.

https://github.com/IAMColumbia/gp2portfoliogame-chopov1/assets/76492881/06e8e2e9-7842-446c-8156-6a9fc64efeda