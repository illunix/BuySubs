terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

resource "tls_private_key" "vpn_client" {
  algorithm = "RSA"
}

resource "tls_self_signed_cert" "vpn_client" {
  private_key_pem = tls_private_key.vpn_client.private_key_pem

  subject {
    common_name  = "buysubs.com"
  }

  validity_period_hours = 12

  allowed_uses = [
    "key_encipherment",
    "digital_signature",
    "server_auth",
  ]
}

resource "aws_acm_certificate" "vpn_server" {
  domain_name       = "buysubs.com"
  validation_method = "DNS"

  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_acm_certificate_validation" "vpn_server" {
  certificate_arn = aws_acm_certificate.vpn_server.arn
}

resource "aws_acm_certificate" "vpn_client_root" {
  private_key = tls_private_key.vpn_client.private_key_pem
  certificate_body = tls_self_signed_cert.vpn_client.cert_pem
}