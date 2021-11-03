#pragma once
class Game
{
public:
	Game();

	void Run();

private:

	virtual void Init() = 0;
	virtual void ShutDown() = 0;
	virtual bool IsGameRunning() = 0;

	virtual void Update() = 0;
	virtual void Draw() = 0;
};

