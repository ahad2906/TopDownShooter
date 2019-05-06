using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
	int DamageAmount ();

	DamageType DamageType ();
}

//Kan bruges til at definere forskellige slags elementeter
public enum DamageType : byte {
	Kinetic,
	Fire,
	Electric
}
