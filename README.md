# Better To Know These Before You Start Reviewing The Game

  Sword object which is has Snapper component could be grabbable and moved around by mouse input. If there is a object contains snapslot component in certain distance it will snap and be child of it. During this event Player's idle animation continues as it is with sword in hand. It doesn't matter which hand has snapslot component is animation continues.
  
  If you put the sword at left hand, for the correct attack animation to play you should tick the "Sword Left Handed" bool in the PlayerCombatController on the Player GameObject.
  
  Some of the additional features, the player can move around and perform combo attacks to dummy, particle effects etc. 
