#!/bin/bash

dotnet ef database update

exec dotnet GOF.Host.dll