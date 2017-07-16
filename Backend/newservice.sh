#!/bin/bash

if [ $# -ne 1 ]; then
    echo $0: Usage: Service Name
    exit 1
fi

name=$1

# create service
pushd Source > /dev/null
mkdir $name
cp ExampleService/aws-lambda-tools-defaults.json $name/
cp ExampleService/Function.cs $name/
cp ExampleService/project.json $name/

pushd $name > /dev/null
sed -i '' -e "s/ExampleService/$name/g" aws-lambda-tools-defaults.json
sed -i '' -e "s/ExampleService/$name/g" Function.cs

popd > /dev/null
popd > /dev/null

# create service test
pushd Test > /dev/null
testname="$name.Test"
mkdir $testname
cp ExampleService.Test/FunctionTest.cs $testname/
cp ExampleService.Test/project.json $testname/

pushd $testname > /dev/null
sed -i '' -e "s/ExampleService/$name/g" FunctionTest.cs
sed -i '' -e "s/ExampleService/$name/g" project.json

popd > /dev/null
popd > /dev/null

echo "  - TEST_DIR=$name" >> ../.travis.yml
echo 'DONE. Have fun ;)'
