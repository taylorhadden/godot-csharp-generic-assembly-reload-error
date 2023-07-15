using Godot;
using System.Collections.Generic;

public abstract partial class GenericScript<T> : Node3D
{
	protected List<T> someList = new List<T>();
}
