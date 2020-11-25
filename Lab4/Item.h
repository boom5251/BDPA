#pragma once

using namespace System;

public ref struct Item sealed
{
private:
	String^ name;
	int count;

public:
	property String^ Name
	{
		String^ get() { return name; }
		void set(String^ name) { this->name = name; }
	}

	property int Count
	{
		int get() { return count; }
		void set(int count) { this->count = count; }
	}
};

