#pragma once

#include "Order.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace System::Xml::Serialization;

public ref class FileData sealed
{
private:
	XmlSerializer^ serializer;

public:
	FileData();

	property XmlSerializer^ Serializer
	{
		XmlSerializer^ get() { return serializer; }
		void set(XmlSerializer^ value) { serializer = value; }
	}

	void SaveData(String^ path, List<Order^>^ orders);
	List<Order^>^ GetData(String^ path);
};

