# meteoroids
 A very simple Asteroids-style shooter, created in Unity 2021 Mac.
 
I deliberately avoided any kind of optimisation, as I was very curious to see what the impact would be on performance if I stuck to searching/finding/getting what was needed when it was needed, no matter how wasteful.

So every place you would expect there to be optimistion, looking up a component once and then storing the reference, and so on, there isn’t one.
 
Instead there are `GetComponent<>` and `FindObjectByType<>` and `FindObjectsByType<>` everywhere.

https://user-images.githubusercontent.com/10506323/219107337-2757a7ad-ea0a-4227-bb6d-52e1ad50026d.mp4

