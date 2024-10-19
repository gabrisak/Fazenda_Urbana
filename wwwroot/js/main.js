(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();

    // Validação de formulários (Cadastro, Esqueceu a Senha, Checkout)
    $('form').on('submit', function (e) {
        var isValid = true;
        var formType = $(this).data('form-type');

        // Verificação geral de campos vazios
        $(this).find('input[required], textarea[required]').each(function () {
            if ($(this).val() === '') {
                isValid = false;
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
            }
        });

        // Validação adicional para formulários de cadastro e recuperação de senha
        if (formType === 'cadastro') {
            var password = $('input[type="password"]').first().val();
            var confirmPassword = $('input[type="password"]').last().val();
            if (password !== confirmPassword) {
                isValid = false;
                alert('As senhas não coincidem.');
            }
        } else if (formType === 'esqueci-senha') {
            var email = $('input[type="email"]').val();
            if (email === '') {
                isValid = false;
                $('input[type="email"]').addClass('is-invalid');
                alert('Por favor, preencha o campo de e-mail.');
            } else {
                $('input[type="email"]').removeClass('is-invalid');
            }
        }

        if (!isValid) {
            e.preventDefault(); // Impede o envio do formulário se inválido
        }
    });

    // Validação em tempo real para o campo de e-mail
    $('input[type="email"]').on('input', function () {
        var email = $(this).val();
        if (email === '') {
            $(this).addClass('is-invalid');
        } else {
            $(this).removeClass('is-invalid');
        }
    });

    // Validação em tempo real para o campo de senha
    $('input[type="password"]').on('input', function () {
        var password = $('input[type="password"]').first().val();
        var confirmPassword = $('input[type="password"]').last().val();
        if (password !== confirmPassword) {
            $('input[type="password"]').last().addClass('is-invalid');
        } else {
            $('input[type="password"]').last().removeClass('is-invalid');
        }
    });

    // Spinner ao enviar o formulário
    $('form').on('submit', function () {
        $('#spinner').addClass('show');
    });

    // Fixed Navbar
    $(window).scroll(function () {
        if ($(window).width() < 992) {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow');
            } else {
                $('.fixed-top').removeClass('shadow');
            }
        } else {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow').css('top', -55);
            } else {
                $('.fixed-top').removeClass('shadow').css('top', 0);
            }
        }
    });

    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });

    // Testimonial carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 2000,
        center: false,
        dots: true,
        loop: true,
        margin: 25,
        nav: true,
        navText: [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsiveClass: true,
        responsive: {
            0: { items: 1 },
            576: { items: 1 },
            768: { items: 1 },
            992: { items: 2 },
            1200: { items: 2 }
        }
    });

    // Vegetable carousel
    $(".vegetable-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1500,
        center: false,
        dots: true,
        loop: true,
        margin: 25,
        nav: true,
        navText: [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsiveClass: true,
        responsive: {
            0: { items: 1 },
            576: { items: 1 },
            768: { items: 2 },
            992: { items: 3 },
            1200: { items: 4 }
        }
    });

    // Função para formatar o valor monetário
    function formatarMoeda(valor) {
        return 'R$' + valor.toFixed(2).replace('.', ',');
    }

    // Atualizar o total ao alterar a quantidade
    function updateCartTotal() {
        var total = 0;
        $('tbody tr').each(function () {
            // Extrair o preço e substituir qualquer vírgula por ponto
            var priceText = $(this).find('.price').text().replace('R$', '').trim();
            var price = parseFloat(priceText.replace('.', '').replace(',', '.')); // Formatação correta para o preço

            // Verificar se o preço foi capturado corretamente
            if (isNaN(price)) {
                price = 0;
            }

            // Capturar a quantidade do input
            var quantity = parseInt($(this).find('.quantity-input').val());
            if (isNaN(quantity) || quantity < 1) {
                quantity = 1; // Definir valor mínimo de 1
            }

            // Calcular o subtotal (preço * quantidade)
            var subtotal = price * quantity;

            // Atualizar o subtotal no HTML
            $(this).find('.total').text(formatarMoeda(subtotal));

            // Somar ao total geral do carrinho
            total += subtotal;
        });

        var frete = 3.00; // Valor do frete fixo
        total += frete;

        // Atualizar o total geral no HTML
        $('.total-carrinho').text(formatarMoeda(total));
        $('.subtotal').text(formatarMoeda(total - frete));
    }

    // Evento para os botões de aumentar e diminuir quantidade
    $('.btn-plus').on('click', function () {
        var input = $(this).closest('.input-group').find('.quantity-input');
        var value = parseInt(input.val());
        input.val(value + 1);
        updateCartTotal();
    });

    $('.btn-minus').on('click', function () {
        var input = $(this).closest('.input-group').find('.quantity-input');
        var value = parseInt(input.val());
        if (value > 1) {
            input.val(value - 1);
            updateCartTotal();
        }
    });

    // Remover item do carrinho
    $('.btn-remove').on('click', function () {
        $(this).closest('tr').remove();
        updateCartTotal();
    });

    // Aplicar cupom (Exemplo básico)
    $('.btn-aplicar-cupom').on('click', function () {
        var cupomCode = $('#cupomCode').val(); // Seletor correto para o campo de cupom
        if (cupomCode === 'DESCONTO10') {
            var total = parseFloat($('.total-carrinho').text().replace('R$', '').replace('.', '').replace(',', '.'));
            var desconto = total * 0.10; // Aplica 10% de desconto
            total = total - desconto;
            $('.total-carrinho').text(formatarMoeda(total));
            alert('Cupom aplicado com sucesso!');
        } else {
            alert('Cupom inválido.');
        }
    });

    // Apenas um método de pagamento pode ser selecionado
    $('input[type="checkbox"][name="Payments"]').on('change', function () {
        $('input[type="checkbox"][name="Payments"]').not(this).prop('checked', false);
    });

})(jQuery);
