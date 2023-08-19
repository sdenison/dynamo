#!/bin/bash

dotnet restore Source/Ffe.sln
dotnet test --logger trx --results-directory ./testresults