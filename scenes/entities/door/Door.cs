using Godot;

public partial class Door : RigidBody2D
{

	[Export] public PhysicsBody2D ConnectedBody;

	private float _initialRotation = 0f;

	public override void _Ready()
	{
		_initialRotation = Rotation;

		var pinJoint2D = GetNode<PinJoint2D>("PinJoint2D");

		Callable.From(() =>
		{
			pinJoint2D.NodeA = pinJoint2D.GetPathTo(ConnectedBody);
		}).CallDeferred();
	}

	public override void _Process(double delta)
	{
		float angleDifference = Mathf.AngleDifference(Rotation, _initialRotation);
		float returnStrength = 8000f;
		float damping = 5f;

		// 2. Apply torque to rotate back to target
		// We use AngularVelocity to add damping, preventing endless swinging
		float torque = angleDifference * returnStrength - AngularVelocity * damping;

		ApplyTorque(torque);
	}
}
