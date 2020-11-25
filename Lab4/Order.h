#pragma once

#include "Item.h"

using namespace System;
using namespace System::Collections::Generic;

[Serializable]
public ref struct Order sealed
{
private:
	int number;
	String^ fullName;
	List<Item^>^ goods = gcnew List<Item^>(); // Список названий товаров + количества
	int amount;

public:
	property int Number
	{
		int get() { return number; }
		void set(int value) { number = value; }
	}

	property String^ FullName
	{
		String^ get() { return fullName; }
		void set(String^ value) { fullName = value; }
	}

	property List<Item^>^ Goods
	{
		List<Item^>^ get() { return goods; }
	}

	property int Amount
	{
		int get() { return amount; }
		void set(int value) { amount = value; }
	}
};