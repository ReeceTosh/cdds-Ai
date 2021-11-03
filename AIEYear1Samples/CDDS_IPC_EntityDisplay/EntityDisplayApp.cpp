#include "EntityDisplayApp.h"
#include <iostream>

using namespace std;

EntityDisplayApp::EntityDisplayApp(int screenWidth, int screenHeight) : m_screenWidth(screenWidth), m_screenHeight(screenHeight) {

}

EntityDisplayApp::~EntityDisplayApp() {

}

bool EntityDisplayApp::Startup() {

	InitWindow(m_screenWidth, m_screenHeight, "EntityDisplayApp");
	SetTargetFPS(60);
	OpenFileMappingEntities();

	return true;
}

void EntityDisplayApp::Shutdown() {

	CloseWindow();        // Close window and OpenGL context
}

void EntityDisplayApp::Update(float deltaTime) {

}

void EntityDisplayApp::Draw() {
	BeginDrawing();

	ClearBackground(RAYWHITE);

	//READ
	char* data = (char*)MapViewOfFile(fileHandle, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(size_t));
	if (data)
	{
		size_t entityCount;
		memcpy(&entityCount, data, sizeof(size_t));
	    UnmapViewOfFile(data);

		data = (char*)MapViewOfFile(fileHandle, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(size_t));
		if (data)
		{
			m_entities.resize(entityCount);
			memcpy(m_entities.data(), data + sizeof(size_t), sizeof(Entity) * entityCount);
			UnmapViewOfFile(data);
		}
	}

	// draw entities
	for (auto& entity : m_entities) {
		DrawRectanglePro(
			Rectangle{ entity.x, entity.y, entity.size, entity.size }, // rectangle
			Vector2{ entity.size / 2, entity.size / 2 }, // origin
			entity.rotation,
			Color{ entity.r, entity.g, entity.b, 255 });
	}

	// output some text, uses the last used colour
	DrawText("Press ESC to quit", 630, 15, 12, LIGHTGRAY);

	EndDrawing();
}

void EntityDisplayApp::OpenFileMappingEntities()
{
	// IN APPLICATION 2 –A user of the named shared memory
	// gain access to a named shared memory block that already exists
	fileHandle = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, L"EntityDisplayApp");
}


	//Entity* data = (Entity)MapViewOfFile(fileHandle, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(Entity));

	//// write to the memory block
	//*data = m_entities;
	//// write out what is in the memory block
	//std::cout <<"EntityData = { ";
	//std::cout <<data->x <<", ";
	//std::cout <<data->rotation <<", ";
	//std::cout <<data->speed <<", ";
	//std::cout <<data->r <<", ";
	//std::cout <<data->size <<", ";
	//std::cout <<" };" << std::endl;


void CloseMapping(HANDLE hObject)
{
	CloseHandle(hObject);
}