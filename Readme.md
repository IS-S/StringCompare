# `SomeProject`

## Сборка и публикация docker image

### CI/CD

Для успешной сборки образа в gitlab pipeline необходимо в [групповых переменных](https://devplatform.tcsbank.ru/ofa/variables) CI/CD задать переменные:
- `DOCKER_USERNAME` - пользователь для docker registry
- `DOCKER_PASSWORD` - пароль для docker registry

Для успешнего деплоя в Kubernetes необходимо в [групповых переменных](https://devplatform.tcsbank.ru/ofa/variables) CI/CD задать переменные:
- `K8S_USERNAME` - пользователь для Kubernetes
- `K8S_PASSWORD` - пароль для Kubernetes

По умолчанию все образы докера складываются:
- в [docker-internal.tcsbank.ru/k8s_namespace/SomeProject-web](docker-internal.tcsbank.ru/k8s_namespace/SomeProject-web) для Web.
- в [docker-internal.tcsbank.ru/k8s_namespace/SomeProject-jobs](docker-internal.tcsbank.ru/k8s_namespace/SomeProject-jobs) для Jobs.

Чтобы переопределить проект в [docker-internal.tcsbank.ru](docker-internal.tcsbank.ru) используете env переменную
`IMAGE_NAME`, которую можно определить в [переменных проекта](https://devplatform.tcsbank.ru/ofa/repositories/view/compare-strings) либо непосредственно в файле .gitlab-ci.yml
