<%@ Page Title="CINEL - Home" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Projeto_Final.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Carousel Start -->
    <div class="container-fluid p-0 pb-5 mb-5">
        <div id="header-carousel" class="carousel slide carousel-fade" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#header-carousel" data-slide-to="0" class="active"></li>
                <li data-target="#header-carousel" data-slide-to="1"></li>
                <li data-target="#header-carousel" data-slide-to="2"></li>
            </ol>
            <div class="carousel-inner">
                <div class="carousel-item active" style="min-height: 300px;">
                    <img class="position-relative w-100" src="img/carousel-1.jpg" style="min-height: 300px; object-fit: cover;">
                    <div class="carousel-caption d-flex align-items-center justify-content-center">
                        <div class="p-5" style="width: 100%; max-width: 900px;">
                            <h5 class="text-white text-uppercase mb-md-3">Melhores Cursos Online</h5>
                            <h1 class="display-3 text-white mb-md-4">Melhor Educação a partir de Casa</h1>
                            <a href="cursos.aspx" class="btn btn-primary py-md-2 px-md-4 font-weight-semi-bold mt-2">eLearning</a>
                        </div>
                    </div>
                </div>
                <div class="carousel-item" style="min-height: 300px;">
                    <img class="position-relative w-100" src="img/carousel-2.jpg" style="min-height: 300px; object-fit: cover;">
                    <div class="carousel-caption d-flex align-items-center justify-content-center">
                        <div class="p-5" style="width: 100%; max-width: 900px;">
                            <h5 class="text-white text-uppercase mb-md-3">Cursos Especialização Tecnológica</h5>
                            <h1 class="display-3 text-white mb-md-4">Melhores Cursos na Vertente Profissional</h1>
                            <a href="cursos.aspx?tipo=cet" class="btn btn-primary py-md-2 px-md-4 font-weight-semi-bold mt-2">CETs</a>
                        </div>
                    </div>
                </div>
                <div class="carousel-item" style="min-height: 300px;">
                    <img class="position-relative w-100" src="img/carousel-3.jpg" style="min-height: 300px; object-fit: cover;">
                    <div class="carousel-caption d-flex align-items-center justify-content-center">
                        <div class="p-5" style="width: 100%; max-width: 900px;">
                            <h5 class="text-white text-uppercase mb-md-3">Oferta Formativa</h5>
                            <h1 class="display-3 text-white mb-md-4">Veja os Cursos que temos para si</h1>
                            <a href="" class="btn btn-primary py-md-2 px-md-4 font-weight-semi-bold mt-2">Lista de Cursos</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Carousel End -->


    <!-- About Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="row align-items-center">
                <div class="col-lg-5">
                    <img class="img-fluid rounded mb-4 mb-lg-0" src="img/logo-cinel.JPG" alt="">
                </div>
                <div class="col-lg-7">
                    <div class="text-left mb-4">
                        <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Sobre Nós</h5>
                        <h1>A Tecnologia e o Futuro num só Centro</h1>
                    </div>
                    <p>
                        O CINEL - Centro de Formação Profissional da Indústria Electrónica, Energia, Telecomunicações e Tecnologias da Informação, é uma entidade de direito público, constituída em 1985 por protocolo celebrado entre o Instituto do Emprego e da Formação Profissional (IEFP,IP) e a Associação Portuguesa das Empresas do Setor Eléctrico e Electrónico (ANIMEE), tendo, em 2011, contado também com a adesão da Associação para a Competitividade Internacionalização Empresarial (ACIE)

Tem sede em Lisboa e uma delegação no Porto e integra, igualmente, a rede de Centros de Gestão Participada do IEFP,IP.

Presta serviços de elevada qualidade a empresas e cidadãos no domínio da qualificação, formação e certificação profissional promovendo a aquisição e/ou o reforço de competências em áreas comportamentais, linguísticas e tecnológicas especialmente em electrónica, robótica, automação e controlo; energias renováveis; telecomunicações; redes, tecnologias e sistemas de informação.

Coloca à disposição dos interessados 22 laboratórios equipados com a mais moderna tecnologia.
                    </p>
                    <a href="about-us.aspx" class="btn btn-primary py-md-2 px-md-4 font-weight-semi-bold mt-2">Saiba Mais</a>
                </div>
            </div>
        </div>
    </div>
    <!-- About End -->

    <!-- Courses Start -->
    <div class="container-fluid py-5">
        <div class="container py-5">
            <div class="text-center mb-5">
                <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Cursos</h5>
                <h1>Os Nossos Cursos Populares</h1>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/programacao.png" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx?curso=programacao">Programação</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">200€</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/multimedia.jpg" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx?curso=multimedia">Multimédia</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">200€</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/robotica.jpg" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx?curso=robotica">Robótica</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">200€</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/redes.jpg" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx?curso=redes">Redes</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">200€</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/ciberseguranca.jpg" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx?curso=ciberseguranca">Cibersegurança</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">200€</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="rounded overflow-hidden mb-2">
                        <img class="img-fluid" src="img/oferta-formativa.jpg" alt="">
                        <div class="bg-secondary p-4">
                            <a class="h5" href="cursos.aspx">Lista de Cursos</a>
                            <div class="border-top mt-4 pt-4">
                                <div class="d-flex justify-content-between">
                                    <h5 class="m-0">Oferta Formativa</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Courses End -->

    <!-- Team Start -->
    <div class="container-fluid py-5">
        <div class="container pt-5 pb-3">
            <div class="text-center mb-5">
                <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px;">Formadores</h5>
                <h1>Conheça a nossa equipa</h1>
            </div>
            <asp:Repeater ID="rpt_formadores" runat="server">
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-md-6 col-lg-3 text-center team mb-4">
                        <div class="team-item rounded overflow-hidden mb-2">
                            <div class="team-img position-relative">
                                <img class="img-fluid" src="img/team-1.jpg" alt="">
                            </div>
                            <div class="bg-secondary p-4">
                                <h5>Jhon Doe</h5>
                                <p class="m-0">Web Designer</p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <div class="row">
                <div class="col-md-6 col-lg-3 text-center team mb-4">
                    <div class="team-item rounded overflow-hidden mb-2">
                        <div class="team-img position-relative">
                            <img class="img-fluid" src="img/team-1.jpg" alt="">
                        </div>
                        <div class="bg-secondary p-4">
                            <h5>Jhon Doe</h5>
                            <p class="m-0">Web Designer</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3 text-center team mb-4">
                    <div class="team-item rounded overflow-hidden mb-2">
                        <div class="team-img position-relative">
                            <img class="img-fluid" src="img/team-2.jpg" alt="">
                        </div>
                        <div class="bg-secondary p-4">
                            <h5>Jhon Doe</h5>
                            <p class="m-0">Web Designer</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3 text-center team mb-4">
                    <div class="team-item rounded overflow-hidden mb-2">
                        <div class="team-img position-relative">
                            <img class="img-fluid" src="img/team-3.jpg" alt="">
                        </div>
                        <div class="bg-secondary p-4">
                            <h5>Jhon Doe</h5>
                            <p class="m-0">Web Designer</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3 text-center team mb-4">
                    <div class="team-item rounded overflow-hidden mb-2">
                        <div class="team-img position-relative">
                            <img class="img-fluid" src="img/team-4.jpg" alt="">
                        </div>
                        <div class="bg-secondary p-4">
                            <h5>Jhon Doe</h5>
                            <p class="m-0">Web Designer</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Team End -->
</asp:Content>
