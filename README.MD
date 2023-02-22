Teste Hyperativa

Foi realizado o testes, com excessão do log e dos testes unitários e de integração, pois não houve tempo para isso.
Porém acredito que o que fiz seja suficiente para uma avaliação das minhas skills.
Contrui com minimal API, mas prefiri não colocar no docker nesse teste, execute no IISExpress por favor.
Assumi que todo arquivo tem o formato do arquivo passado como exemplo.
Não foi feita nenhuma validação de entrada de dados, pois não estava no requisito. E se tivesse que fazer, a complexidade poderia aumentar ainda mais, com decisões de arquitetura.

Como executar

1) Mude a string de conexao no appsettings, para uma base SQL SERVER:

	"HyperativaConnectionString": "Server=DESKTOP-004K27C\\SQLEXPRESS;Database=HyperativaDB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

2) Execute a migrations:

	dotnet ef database update --project TesteHyperativa-Repository
	
3) Execute o visual studio no iisexpress.

4) Ao executar o sistema, ele criará o usuario "edu", com senha "edu", use-o para logar, o request do login é esse:

	{
	  "userName": "edu",
	  "password": "edu"
	}
	
5) Copie o token e o configure no swagger, não esquecer de colocar a palavra "Bearear" +  [space] + Token copiado

6) Executar os endpoints e testar

7) Criptografei os dados sensiveis com MD5, se precisar desincriptografar já tem um método em CryptographyService. A chave configurada é uma genérica, não utilize no mundo real, troque.

Acho que é isso pessoal fico no aguardo.


