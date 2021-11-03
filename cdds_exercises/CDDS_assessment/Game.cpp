#include "Game.h"

Game::Game()
{
}

void Game::Run()
{
	Init();

	while (IsGameRunning())
	{
		Draw();
		Update();
	}

	ShutDown();

}