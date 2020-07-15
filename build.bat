docker build -f src/Techhunt.SalaryManagement.Api/Dockerfile -t nisal/techhunt-api .
cd src/Techhunt.SalaryManagement.Ui
docker build -t nisal/techhunt-web .
cd ..
cd ..