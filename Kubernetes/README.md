# Kubernetes deployment

This folder contains a Kustomize-based deployment for running the Blazor app on Kubernetes.

## What it includes

- `base/`:
  - `Namespace` (`dynamo`)
  - `ServiceAccount` (`dynamo-web`)
  - `Deployment` (`dynamo-web`)
  - `Service` (`ClusterIP`)
  - `Ingress` (configured for AWS Load Balancer Controller / ALB)
- `overlays/test/`: environment-specific image tag and environment variable values

## Prerequisites

- Kubernetes cluster (EKS recommended for parity with current AWS deployment)
- `kubectl` and `kustomize` (or `kubectl apply -k`)
- AWS Load Balancer Controller installed in the cluster (for `Ingress`)
- Image pushed to ECR

## IAM permissions for AWS services (DynamoDB, S3, etc.)

The app currently accesses AWS services directly from code. In EKS, attach an IAM role to the `dynamo-web` service account using IRSA:

1. Create an IAM role with permissions equivalent to the ECS task role.
2. Update `base/serviceaccount.yaml` annotation:
   - `eks.amazonaws.com/role-arn: arn:aws:iam::<account-id>:role/<role-name>`
3. Apply manifests.

## Deploy

```bash
kubectl apply -k Kubernetes/overlays/test
```

## Validate

```bash
kubectl -n dynamo get pods,svc,ingress
kubectl -n dynamo describe ingress dynamo-web
```

## Update image

Update the image tag in `Kubernetes/overlays/test/kustomization.yaml`, then re-apply:

```bash
kubectl apply -k Kubernetes/overlays/test
```

