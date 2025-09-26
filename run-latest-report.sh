#!/bin/bash
export FUNDA_USER_AGENT="Funda Interviewee/1.0.0 (acb19478-b4c0-4aea-b78b-d87af44e8db2)"

echo "🧹 Cleaning old results..."
rm -rf ./allure-report
rm -rf ./bin/Debug/net9.0/allure-results

echo "🚀 Running tests..."
dotnet test

echo "📊 Generating Allure report..."
allure generate ./bin/Debug/net9.0/allure-results --clean -o ./allure-report

echo "🌐 Opening Allure report..."
allure open ./allure-report
