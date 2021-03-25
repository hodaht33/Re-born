@REM Forst, build project
yarn build

@REM Then, remove existing files
rd /s /q "../docs"

@REM Finally, move generated build files to docs.
move "build" "../docs"